using Books.Application.Authentication.Common;
using Books.Application.Contracts.Persistence;
using Books.Application.Contracts.Services;
using Books.Application.Extensions;
using Books.Domain.UserAggregate;
using Books.Shared.Errors.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResponse, Error>>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRespository _userRespository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRespository userRespository, IPasswordHasher<User> passwordHasher)
        {
            _jwtGenerator = jwtGenerator;
            _userRespository = userRespository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result<AuthenticationResponse, Error>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            Result<bool, ValidationError> validationResult = await request.ValidateAsync(new LoginQueryValidator(), cancellationToken);
            if (validationResult.IsFailure)
                return validationResult.Error;

            User? user = await _userRespository.GetUserByEmailAsync(request.Email);
            if (user is null)
                return new InvalidCredentialsError("Invalid email or password");

            PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return new InvalidCredentialsError("Invalid email or password");

            string token = _jwtGenerator.CreateToken(user);

            return new AuthenticationResponse(user, token);


        }
    }
}
