﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, 
            IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListOfAllWalkDifficulties()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllWalkDifficulties();

            var walkDifficulitesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);

            return Ok(walkDifficulitesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultiesById")]
        public async Task<IActionResult> GetWalkDifficultiesById(Guid id) 
        {
            var walkDifficult = await walkDifficultyRepository.GetWalkDifficultyById(id);
            if (walkDifficult == null)
            {
                return NotFound();
            }

            // Convert Domain to DTOs
            var walkDifficultDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficult);

            return Ok(walkDifficultDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(
            AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Convert DTO to Domain model
            var walkDifficultDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            //Call repository
            walkDifficultDomain = await walkDifficultyRepository.
                AddWalkDifficulty(walkDifficultDomain);

            //Convert Domain to DTO 
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultDomain);

            // Return response
            return CreatedAtAction(nameof(GetWalkDifficultiesById), 
                new {id = walkDifficultyDTO.Id}, walkDifficultyDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultiesAsync(Guid
             id, UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Convert DTO to Domain Model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            //Call repository to update
            walkDifficultyDomain = await walkDifficultyRepository.UpdateWalkDifficultyAsync(
                id, walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert Domain to DTO 
            var walkDifficultDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            //Return response
            return Ok(walkDifficultDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulties(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.
                DeleteWalkDifficultyAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert to DTO 
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }
    }
}
