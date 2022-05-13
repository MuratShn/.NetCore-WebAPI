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

        public IDataResult<User> GetUserByMail(string Email)
        {
            var check = _userDal.Get(p => p.Email == Email); //Daha önceden böyle bi mail ile hesap açılmamış ise null dönücek eğer hesap var ise data dönücek

            if ( check == null)
            {
                return new DataResult<User>(check,true); //hesap yok
            }
            return new DataResult<User>(check, false); //Böyle bir hesap var
            
        }


        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccesDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }
    }
}
