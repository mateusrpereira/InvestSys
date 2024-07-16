using Application.Active.Dtos;
using Application.Active.Ports;
using Application.Active.Requests;
using Application.Active.Responses;
using Domain.Active.Exceptions;
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

                //request.Data.Id = await _activeRepository.Create(active);
                await active.Save(_activeRepository);
                request.Data.Id = active.Id;

                return new ActiveResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (MissingRequiredInformation e)
            {
                return new ActiveResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ACTIVE_MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information passed"
                };
            }
            catch (InvalidActiveTypeException e)
            {
                return new ActiveResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ACTIVE_INVALID_TYPE,
                    Message = "The given active type is not valid"
                };
            }
            catch (InvalidCodeException e)
            {
                return new ActiveResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ACTIVE_INVALID_CODE,
                    Message = "The given code is not valid"
                };
            }
            catch (Exception)
            {
                return new ActiveResponse
                { 
                    Success = false,
                    ErrorCode = ErrorCodes.ACTIVE_COULD_NOT_STORE_DATA,
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
        public async Task<ActiveResponse> UpdateActive(UpdateActiveRequest request)
        {
            try
            {
                var active = ActiveDto.MapToEntity(request.Data);

                await active.Save(_activeRepository);
                request.Data.Id = active.Id;

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
                    ErrorCode = ErrorCodes.ACTIVE_UPDATE_FAILED,
                    Message = "There was an error when updating to DB"
                };
            }
        }
        public async Task<ActiveResponse> DeleteActive(int activeId)
        {
            try
            {
                await _activeRepository.Delete(activeId);

                return new ActiveResponse
                {
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new ActiveResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ACTIVE_DELETE_FAILED,
                    Message = "There was an error when deleting to DB"
                };
            }
        }
    }
}
