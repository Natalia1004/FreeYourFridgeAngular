﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FreeYourFridge.API.Data;
using FreeYourFridge.API.Data.Interfaces;
using FreeYourFridge.API.DTOs;
using FreeYourFridge.API.Filters;
using FreeYourFridge.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreeYourFridge.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/dailymeal")]
    public class DailyMealController:ControllerBase
    {
        private readonly IDailyMealRepository _repository;
        private readonly IMapper _mapper;

        public DailyMealController(IDailyMealRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDailyMeals()
        {
            var meals= await _repository.GetDailyMealsAsync();
            var mealsFiltered = meals.Where(dm =>
                dm.CreatedBy == int.Parse(User.FindFirst(claim => 
                    claim.Type == ClaimTypes.NameIdentifier).Value));
            return Ok(_mapper.Map<List<DailyMealBasicDto>>(mealsFiltered));
        }

        [HttpGet]
        [Route("{id}", Name="GetDailyMeal")]
        public async Task<IActionResult> GetSingleDailyMeal(int id)
        {
            var dMeal = await _repository.GetDailyMealAsync(id);
            return Ok(_mapper.Map<DailyMealBasicDto>(dMeal));
        }

        [HttpGet("{id}/details")]
        [DailMealFilter]
        public async Task<IActionResult> GetSingleDailyMealDetails(int id)
        {
            var dMealLocal = await _repository.GetDailyMealAsync(id);
            if (dMealLocal == null) return NotFound();
            var incomMeal = await _repository.GetExternalDailyMeal(id);
            (Models.DailyMeal dMeal, ExternalModels.IncomingRecipe iRecipe) = (dMealLocal, incomMeal);
            return Ok((dMealLocal, incomMeal));
        }

        /// <summary>
        /// add DailyMeal; called only once after addDailyMeal from recipe-detail-component.ts (Angular)
        /// </summary>
        /// <param name="dailyMealToAddDto"></param>
        /// <returns></returns>
        [HttpPost]
        //[Consumes("application/json")]

        public async Task<IActionResult> AddDailyMeal([FromBody] DailyMealToAddDto dailyMealToAddDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var record = await _repository.GetDailyMealAsync(dailyMealToAddDto.Id);
            if (record != null)
            {
                if (record.Id == dailyMealToAddDto.Id)
                {
                    return StatusCode(302);
                }
                await CheckTimeInEntityTable();
            }

            CheckTimeInEntityTable();
            var dMealToAdd = _mapper.Map<Models.DailyMeal>(dailyMealToAddDto);
            var userId = User.FindFirst(claim=>claim.Type == ClaimTypes.NameIdentifier).Value;
            dMealToAdd.TimeOfLastMeal = DateTime.Now;
            dMealToAdd.CreatedBy = int.Parse(userId);
            await _repository.AddMeal(dMealToAdd);
            return CreatedAtRoute("GetDailyMeal", new { dMealToAdd.Id }, null);

        }

        /// <summary>
        /// Updates daily Meal - called by Angular from dailyMeadDetails.component.ts
        /// </summary>
        /// <param name="dailyMealToAddDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDailyMeal([FromBody] DailyMealToAddDto dailyMealToAddDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var dMeal = await _repository.GetDailyMealAsync(dailyMealToAddDto.Id);
            if (dMeal == null) return BadRequest();

            dMeal.Grams = dailyMealToAddDto.Grams;
            dMeal.Title = dailyMealToAddDto.Title;
            dMeal.UserRemarks = dailyMealToAddDto.UserRemarks;

            await _repository.UpdateMeal(dMeal);
            return NoContent();
        }

        //[HttpDelete]
        //public async Task<ActionResult> ClearDailyMeals()
        //{
        //    await _repository.ClearTable();
        //    return NoContent();
        //}

        private async Task CheckTimeInEntityTable()
        {
            var meals = await _repository.GetDailyMealsAsync();
            var lastMeal = meals
                .OrderBy(m => m.TimeOfLastMeal)
                .FirstOrDefault();
            if (lastMeal == null)
            {
                if ((DateTime.Now.DayOfYear-lastMeal.TimeOfLastMeal.DayOfYear)>=1)
                {
                    await _repository.ClearTable();
                }
            }
            if ((DateTime.Now.DayOfYear - lastMeal.TimeOfLastMeal.DayOfYear) >= 1)
            {
                await _repository.ClearTable();
            }

        }
    }
}

