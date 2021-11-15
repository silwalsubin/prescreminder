import Cookies from 'cookies-ts';
const cookies = new Cookies();
const BEARER_TOKEN_COOKIE_KEY = "access_token";

const getBearerToken = () => cookies.get(BEARER_TOKEN_COOKIE_KEY);

const setBearerToken = (bearerToken: string) => cookies.set(BEARER_TOKEN_COOKIE_KEY, bearerToken);

const removeBearerToken = () => cookies.remove(BEARER_TOKEN_COOKIE_KEY);

export {
  getBearerToken,
  setBearerToken,
  removeBearerToken,
}