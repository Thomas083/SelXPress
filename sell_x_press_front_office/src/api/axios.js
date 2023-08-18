import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:7094',
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 6000,
  withCredentials: false,
});

const setRequestConfig = (queryParams) => {
  const source = axios.CancelToken.source();
  let config = {
    cancelToken: source.token,
    params: {},
  };
  if (queryParams) {
    config.params = queryParams;
  }

  return config;
};

export const GET = async (url, queryParams = null) => {
  return instance.get(url, { ...setRequestConfig(queryParams) });
};

export const POST = async (url, data = null, queryParams = null) => {
  return instance.post(url, data, { ...setRequestConfig(queryParams) });
};

export const DELETE = async (url, queryParams = null) => {
  return instance.delete(url, { ...setRequestConfig(queryParams) });
};

export const PUT = async (url, data = null, queryParams = null) => {
  return instance.put(url, data, { ...setRequestConfig(queryParams) });
};

export const PATCH = async (url, data, queryParams = null) => {
  return instance.patch(url, data, { ...setRequestConfig(queryParams) });
};
