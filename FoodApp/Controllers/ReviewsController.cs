﻿using FoodApp.Constants;
using FoodApp.Models.DataTransferObjects;
using FoodApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodApp.Controllers
{
    [Route(RouteConsts.BASE_URL)]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet(RouteConsts.DISHES_REVIEWS_ENDPOINT_URL)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> Get(long dishId)
        {
            return Ok(await _reviewsService.GetAllAsync(dishId));
        }

        [HttpGet(RouteConsts.REVIEWS_ENDPOINT_URL + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewDTO>> GetAll(long id)
        {
            ReviewDTO receivedProvider = await _reviewsService.GetAsync(id);
            if (receivedProvider == null)
                return NotFound();
            return Ok(receivedProvider);
        }

        [HttpPost (RouteConsts.DISHES_REVIEWS_ENDPOINT_URL)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReviewDTO>> Post(long dishId, [FromBody] CreateReviewDTO createRequestDTO)
        {
            return Ok(await _reviewsService.CreateAsync(dishId, createRequestDTO));
        }

        [HttpPut(RouteConsts.REVIEWS_ENDPOINT_URL + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewDTO>> Put(long id, [FromBody] CreateReviewDTO updateRequestDTO)
        {
            ReviewDTO updatedProvider = await _reviewsService.UpdateAsync(id, updateRequestDTO);
            if (updatedProvider == null)
                return NotFound();
            return Ok(updatedProvider);
        }

        [HttpDelete(RouteConsts.REVIEWS_ENDPOINT_URL + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            if (!await _reviewsService.DeleteAsync(id))
                return NotFound();
            return NoContent();
        }
    }
}