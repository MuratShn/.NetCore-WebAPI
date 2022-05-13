using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {

        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccesResult(Messages.ProductAdded);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var test = _userDal.Get(p => p.Email == email);
            var b = new SuccesDataResult<User>(_userDal.Get(p => p.Email == email));
            return new SuccesDataResult<User>(_userDal.Get(p => p.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccesDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }
    }
}
