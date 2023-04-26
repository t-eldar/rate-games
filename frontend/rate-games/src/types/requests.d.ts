export type SignInRequest = {
  email: string;
  password: string;
  rememberMe: boolean;
};

export type SignUpRequest = {
  name: string;
  surname: string;
  email: string;
  password: string;
};
