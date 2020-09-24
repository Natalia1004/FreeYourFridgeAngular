﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using FreeYourFridge.API.Data.Interfaces;
using FreeYourFridge.API.ExternalModels;
using FreeYourFridge.API.Helpers;
using FreeYourFridge.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace FreeYourFridge.API.Data
{
    public class DailyMealRepository : IDailyMealRepository
    {
        private readonly DataContext _context;
        private const string UrlToSpoon = "https://api.spoonacular.com/recipes/";
        private const string QueryContent = "information?includeNutrition=true&";
        private readonly ApiKeyReader _apiKeyReader;
        

        public DailyMealRepository(DataContext context,
            ApiKeyReader apiKeyReader)
        {
            _context = context;
            _apiKeyReader = apiKeyReader;
        }

        /// <summary>
        /// Gets a list of DailyMeals from localDB
        /// </summary>
        /// <returns>a list of DailyMeals</returns>
        public async Task<IEnumerable<DailyMeal>> GetDailyMealsAsync() => await _context.DailyMeals.ToListAsync();

        /// <summary>
        /// Gets one DailyMeal from localDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>single DailyMeal</returns>
        public async Task<DailyMeal> GetDailyMealAsync(int id) => await _context.DailyMeals.FirstOrDefaultAsync(m => m.Id == id);

        /// <summary>
        /// Add meal to localDB
        /// </summary>
        /// <param name="dailyMeal"></param>
        /// <returns>void</returns>
        public async Task AddMeal(DailyMeal dailyMeal)
        {
            if (dailyMeal == null)
            {
                throw new ArgumentNullException(nameof(dailyMeal));
            } 
            _context.DailyMeals.Add(dailyMeal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMeal(DailyMeal meal)
        {
            _context.DailyMeals.Update(meal);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes all elements from the entity "DailyMeals"
        /// </summary>
        /// <returns>void</returns>
        public async Task ClearTable()
        {
            _context.DailyMeals.RemoveRange();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Pulls single meal from Api.Spoonacular
        /// </summary>
        /// <param name="id"> it's SpoonacularId</param>
        /// <returns>deserialized class IncomingRecipe</returns>
        public async Task<IncomingRecipe> GetExternalDailyMeal(int id) 
        {
            var client = new RestClient($"{UrlToSpoon}/{id}/{QueryContent}{_apiKeyReader.getKey()}");
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync<IncomingRecipe>(request);
            if (response.IsSuccessful)
            {
                return null;
            }
            return response.Data;
        }
    }
}