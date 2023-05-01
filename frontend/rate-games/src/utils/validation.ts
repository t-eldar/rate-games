const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$/;
export const isPasswordValid = (password: string) =>
  passwordRegex.test(password);

const usernameRegex = /^[a-zA-Z0-9_.\-+]+$/;
export const isUsernameValid = (username: string) =>
  usernameRegex.test(username);
