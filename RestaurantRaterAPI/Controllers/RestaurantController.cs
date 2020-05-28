﻿using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get ()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetByRestaurantId(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant model)
        {
            if (ModelState.IsValid)
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);
                if(restaurant != null)
                {
                    restaurant.Name = model.Name;
                    restaurant.Address = model.Address;
                    restaurant.Rating = model.Rating;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurantByRestaurantId(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            _context.Restaurants.Remove(restaurant);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
