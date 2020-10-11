using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CaptchaApp.Server.Models;
using CaptchaApp.Server.Services;
using CaptchaApp.Server.Services.Implementation;
using CaptchaApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CaptchaApp.Server.Controllers
{
    /// <summary>
    /// Контроллер наборов данных разпознавания.
    /// </summary>
    [Route("api/captcha")]
    // NOTE можно подключить swagger (не успеваю).
    // NOTE нужно подключить авторизацию (но не успеваю).
    public class CaptchaController : ControllerBase
    {
        readonly IDbRepository _dbRepository;
        readonly IDataSetFileService _fileService;
        readonly ILogger<CaptchaController> _logger;
        readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dbRepository">Репозитория БД.</param>
        /// <param name="fileService">Сервис файлов.</param>
        /// <param name="logger">Логгер.</param>
        /// <param name="mapper">Маппер.</param>
        public CaptchaController(IDbRepository dbRepository, IDataSetFileService fileService,
            ILogger<CaptchaController> logger, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _fileService = fileService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех наборов данных распознавания.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(IEnumerable<CaptchaDataSetViewModel>))]
        public async Task<ActionResult<IEnumerable<CaptchaDataSetViewModel>>> GetAll()
        {
            var entities = await _dbRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CaptchaDataSetViewModel>>(entities));
        }

        /// <summary>
        /// Получить набор данных распознавания по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор модуля распознавания.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(typeof(CaptchaDataSetViewModel))]
        public async Task<ActionResult<CaptchaDataSetViewModel>> GetById(Guid id)
        {
            var entitiy = await _dbRepository.GetById(id);
            if (entitiy == null)
                return NotFound();

            return _mapper.Map<CaptchaDataSetViewModel>(entitiy);
        }

        /// <summary>
        /// Добавить набор данных разпознавания.
        /// </summary>
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var files = Request.Form.Files?.ToList();

            if (files == null || files.Count == 0)
                return BadRequest("Не указаны файлы для загрузки.");

            if (files.Count != 1)
                return BadRequest("Может быть загружен только 1 файл.");

            // Т.к. в одном запросе и файл, и модель, то "достать" модель так.
            var modelJson = Request.Form["model"].First();
            var postModel = JsonConvert.DeserializeObject<CaptchaDataSetPostModel>(modelJson);

            if (!TryValidateModel(postModel))
                return BadRequest(ModelState);
            
            var file = files.First();

            // Добавить файл-архив.
            var fileErrors = await _fileService.AddFileAndValidate(file, postModel);

            if (fileErrors.Any())
                return BadRequest(string.Join("\r\n", fileErrors));

            var dataSet = _mapper.Map<CaptchaDataSet>(postModel);
            dataSet.ImagesZipFilename = file.FileName;
            await _dbRepository.Add(dataSet);
            var viewModel = _mapper.Map<CaptchaDataSetViewModel>(dataSet);

            _logger.LogInformation("Сохранён набор данных: {0}", JsonConvert.SerializeObject(viewModel));

            return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, viewModel);
        }
    }
}
