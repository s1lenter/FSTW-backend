using AutoMapper;
using FSTW_backend.Dto;
using FSTW_backend.Models;
using Microsoft.AspNetCore.Identity;
using Profile = FSTW_backend.Models.Profile;

namespace FSTW_backend.Mapping
{
    public static class AuthUserMapper
    {
        public static User Map(UserRegisterRequestDto userDto)
        {
            var user = new User();
            user.Login = userDto.Login;
            user.Email = userDto.Email;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, userDto.Password);
            user.CreatedDate = DateTime.UtcNow;
            return user;
        }

        public static TDto MapProfile<TEntity, TDto>(TEntity entity) 
            where TDto : new()
        {
            var dto = new TDto();
            var dtoProps = dto.GetType().GetProperties();
            var entityProps = entity.GetType().GetProperties();

            foreach (var prop in dtoProps)
            {
                var entityProp = entityProps.FirstOrDefault(p => p.Name == prop.Name &&
                                                                 p.PropertyType == prop.PropertyType);
                if (entityProp is not null)
                    prop.SetValue(dto, entityProp.GetValue(entity));

            }
            return dto;
        }

        public static TEntity MapProfileReverse<TDto, TEntity>(TDto dto)
            where TEntity : new()
        {
            var entity = new TEntity();
            var dtoProps = entity.GetType().GetProperties();
            var entityProps = dto.GetType().GetProperties();

            foreach (var prop in dtoProps)
            {
                var entityProp = entityProps.FirstOrDefault(p => p.Name == prop.Name &&
                                                                 p.PropertyType == prop.PropertyType);
                if (entityProp is not null)
                    prop.SetValue(entity, entityProp.GetValue(dto));

            }
            return entity;
        }
    }
}
