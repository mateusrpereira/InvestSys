using Application.Active.Dtos;
using Application.Active.Ports;
using Application.Active.Requests;
using Application.Active.Responses;
using Domain.Active.Ports;

namespace Application.Active
{
    public class ActiveManager : IActiveManager
    {
        private IActiveRepository _activeRepository;
        public ActiveManager(IActiveRepository activeRepository)
        {
            _activeRepository = activeRepository;
        }
        public async Task<ActiveResponse> CreateActive(CreateActiveRequest request)
        {
            try
            {
                var active = ActiveDto.MapToEntity(request.Data);

                request.Data.Id = await _activeRepository.Create(active);

                return new ActiveResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new ActiveResponse
                { 
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<ActiveResponse> GetActive(int activeId)
        {
            var active = await _activeRepository.Get(activeId);

            if (active == null)
            {
                return new ActiveResponse 
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ACTIVE_NOT_FOUND,
                    Message = "No Active record was found with the given Id"
                };
                
            }

            return new ActiveResponse 
            {
                Data = ActiveDto.MapToDto(active),
                Success = true,
            };
        }
    }
}
