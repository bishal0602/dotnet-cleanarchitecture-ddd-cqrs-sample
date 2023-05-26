import axios from "axios";
import { User } from "../types/AuthTypes";
import { AuthResponse } from "../types/ApiTypes";

const API_SERVER = "http://localhost:5263";

export const sendLoginRequest = async (
  email: string,
  password: string,
  login: (data: User) => Promise<void>
) => {
  const response = await axios.post(`${API_SERVER}/api/auth/login`, {
    email,
    password,
  });
  if (response.status === 200) {
    const content = response.data as AuthResponse;
    const user = {
      id: content.user.id,
      firstname: content.user.firstName,
      lastname: content.user.lastName,
      username: content.user.userName,
      email: content.user.email,
      token: content.token,
    };
    await login(user);
  }
};
