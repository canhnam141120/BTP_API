﻿using BTP_API.Helpers;

namespace BTP_API.ServicesImpl
{
    public interface IManageBookRepository
    {
        public Task<ApiResponse> getAllBookAsync(int page = 1);
        public Task<ApiResponse> getAllBookApprovedAsync(int page = 1);
        public Task<ApiResponse> getAllBookDeniedAsync(int page = 1);
        public Task<ApiResponse> getAllBookWaitingAsync(int page = 1);
        public Task<ApiResponse> getBookByIdAsync(int bookId);
        public Task<ApiResponse> searchBookAsync(string search, int page = 1);
        public Task<ApiMessage> approvedBookAsync(int bookId);
        public Task<ApiMessage> deniedBookAsync(int bookId);
        public Task<ApiResponse> getFeedbackInBookAsync(int bookId);
        public Task<ApiMessage> deleteFeedbackAsync(int feedbackId);
        public Task<ApiResponse> totalBookAndUserAsync();
    }
}
