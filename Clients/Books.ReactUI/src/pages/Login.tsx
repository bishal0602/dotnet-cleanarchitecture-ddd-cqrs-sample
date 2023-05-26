import { Navigate, useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import { useEffect, useState } from "react";
import { LoginModel } from "../types/AuthTypes";
import { sendLoginRequest } from "../services/userService";

const Login = () => {
  const { user, login } = useAuth();
  const navigate = useNavigate();
  const [loginModel, setLoginModel] = useState<LoginModel>({
    email: "",
    password: "",
  } as LoginModel);
  const [isEmailError, setIsEmailError] = useState(false);
  const [isPasswordError, setIsPasswordError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
    if (user) navigate("/");
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSubmit = async () => {
    setIsEmailError(false);
    setIsPasswordError(false);
    const email = loginModel.email.trim();
    const password = loginModel.password.trim();
    if (!email && !password) {
      setIsEmailError(true);
      setIsPasswordError(true);
      return;
    }
    if (!email) {
      setIsEmailError(true);
      return;
    }
    if (!password) {
      setIsPasswordError(true);
      return;
    }
    try {
      setIsLoading(true);
      setIsEmailError(false);
      setIsPasswordError(false);
      await sendLoginRequest(email, password, login);
      setLoginModel({ email: "", password: "" });
    } catch (ex) {
      setIsEmailError(true);
      setIsPasswordError(true);
    } finally {
      setIsLoading(false);
    }
  };

  if (user) {
    <Navigate to="/" />;
  }
  return (
    <div className="flex items-center justify-center flex-grow">
      <form
        className="px-8 pt-6 pb-8 mb-4 shadow-xl rounded-2xl"
        onSubmit={async (e) => {
          e.preventDefault();
          await handleSubmit();
        }}
      >
        <div className="mb-4">
          <label className="input-group">
            <span
              className={`w-[30%] ${isEmailError ? "bg-error" : "bg-primary"}`}
            >
              Email
            </span>
            <input
              type="text"
              placeholder="Your email"
              autoComplete="email"
              value={loginModel.email}
              className="input input-bordered"
              onChange={(e) => {
                setLoginModel({ ...loginModel, email: e.target.value });
              }}
            />
          </label>
        </div>
        <div className="mb-6">
          <label className="input-group">
            <span
              className={`w-[30%]  ${
                isPasswordError ? "bg-error" : "bg-primary"
              }`}
            >
              Password
            </span>
            <input
              type="password"
              placeholder="Password"
              autoComplete="current-password"
              value={loginModel.password}
              className="input input-bordered"
              onChange={(e) => {
                setLoginModel({ ...loginModel, password: e.target.value });
              }}
            />
          </label>
        </div>
        <div className="flex items-center justify-center">
          <button
            disabled={isLoading}
            className="btn btn-active btn-neutral w-[40%] text-base"
            type="submit"
          >
            Sign In
          </button>
        </div>
      </form>
    </div>
  );
};
export default Login;
