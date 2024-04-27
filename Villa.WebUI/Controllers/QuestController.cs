using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Villa.Business.Abstract;
using Villa.Dto.Dtos.QuestDtos;
using Villa.Entity.Entities;

namespace Villa.WebUI.Controllers
{
    public class QuestController : Controller
    {
        private readonly IQuestService _bannerService;
        private readonly IMapper _mapper;

        public QuestController(IQuestService bannerService, IMapper mapper)
        {
            _bannerService = bannerService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _bannerService.TGetAllAsync();
            var bannerList = _mapper.Map<List<ResultQuestDto>>(values);
            return View(bannerList);

        }


        public async Task<IActionResult> DeleteQuest(ObjectId id)
        {
            await _bannerService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult CreateQuest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuest(CreateQuestDto createQuestDto)
        {
            var newQuest = _mapper.Map<Quest>(createQuestDto);
            await _bannerService.TCreateAsync(newQuest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateQuest(ObjectId id)
        {
            var value = await _bannerService.TGetByIdAsync(id);
            var updateQuest = _mapper.Map<UpdateQuestDto>(value);
            return View(updateQuest);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuest(UpdateQuestDto updateQuestDto)
        {
            var banner = _mapper.Map<Quest>(updateQuestDto);
            await _bannerService.TUpdateAsync(banner);
            return RedirectToAction("Index");
        }

    }
}
