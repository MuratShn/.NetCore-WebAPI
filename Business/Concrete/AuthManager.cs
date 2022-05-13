using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        //Kayıt olmak için gerekli olan operasonu gerceklestırıyor
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password) //Kullanıcı bılgılerını ve passwordunu(dto ıcınded var) aldık
        {

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt); //Burda Şifreyi Hashliyoruz

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            }; //bilgileri eşleştiriyoruz ve altta ekleyip result donuyoruz
            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Kullanıcı Ekleme Başarılı");
        }

        //Giriş yaparken gerekli olan operasonu gerceklestırıyor
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetUserByMail(userForLoginDto.Email);

            if (userToCheck.succes) //Böyle bir kullanıcı yok
            {
                return new ErrorDataResult<User>("Kullanıcı Bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>("Şifre Yanlış");
            }

            return new SuccesDataResult<User>(userToCheck.Data,true, "Giriş başarılı");
        }
        
        //Çıkış yaparken gerekli olan operasonu gerceklestırıyor
        public IResult UserExists(string email)
        {
            var userToCheck = _userService.GetUserByMail(email);

            if (userToCheck.succes)
            {
                return new SuccesResult();
            }
            return new ErrorResult("Kullanıcı Mevcut");

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccesDataResult<AccessToken>(accessToken, true,"Token Oluşturuldu");
        }
    }
}
