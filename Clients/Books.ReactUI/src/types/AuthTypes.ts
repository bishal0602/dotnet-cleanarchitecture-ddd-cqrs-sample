export type User = {
  id: string;
  username: string;
  firstname: string;
  lastname: string;
  email: string;
  token: string;
};

export type LoginModel = {
  email: string;
  password: string;
};
