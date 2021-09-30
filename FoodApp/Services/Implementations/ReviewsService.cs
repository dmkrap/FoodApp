﻿using AutoMapper;
using FoodApp.Models;
using FoodApp.Models.DataTransferObjects;
using FoodApp.Repositories;
using FoodApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Services.Implementations
{
    public class ReviewsService : IReviewsService
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IMapper _mapper;

        public ReviewsService(IDishesRepository dishesRepository, IReviewsRepository reviewsRepository, IMapper mapper)
        {
            _dishesRepository = dishesRepository;
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
        }

        public async Task<ReviewDTO> GetAsync(long id)
        {
            Review outResultEntity = await _reviewsRepository.GetAsync(id);
            return _mapper.Map<ReviewDTO>(outResultEntity);
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllAsync(long dishId)
        {
            if (!await _dishesRepository.Exists(dishId))
                return null;
            IEnumerable<Review> outResultEntities = await _reviewsRepository.GetAllAsync(dishId);
            return _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(outResultEntities);
        }

        public async Task<ReviewDTO> CreateAsync(long dishId, CreateReviewDTO createRequestDTO)
        {
            if (!await _dishesRepository.Exists(dishId))
                return null;
            Review reviewEntity = _mapper.Map<Review>(createRequestDTO);
            reviewEntity.DishID = dishId;
            reviewEntity = await _reviewsRepository.CreateAsync(reviewEntity);
            return _mapper.Map<ReviewDTO>(reviewEntity);
        }

        public async Task<ReviewDTO> UpdateAsync(long id, CreateReviewDTO updateRequestDTO)
        {
            Review reviewEntity = _mapper.Map<Review>(updateRequestDTO);
            reviewEntity.Id = id;
            reviewEntity = await _reviewsRepository.UpdateAsync(reviewEntity);
            return _mapper.Map<ReviewDTO>(reviewEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _reviewsRepository.DeleteAsync(id);
        }
    }
}