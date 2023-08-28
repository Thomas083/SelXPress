export const ENDPOINTS = {
    // AUTH
    USER_LOGIN: '/api/User/auth/login', //(POST)

    // USER
    GET_ALL_USER: '/api/User', //(GET)
    GET_ONE_USER: '/api/User/email', //(GET)
    CREATE_USER: '/api/User', //(POST)
    LOGOUT_USER: '/api/User/logout', //(DELETE)
    UPDATE_USER: '/api/User/email', //(PUT)
    REFRESH_TOKEN: '/api/User/refreshToken', //(POST)

    // CATEGORY
    GET_ALL_CATEGORY: '/api/Category', //(GET)
    UPDATE_CATEGORY: '/api/Category', //(PUT) + add ID

    // TAG
    GET_ALL_TAG: '/api/Tag', //(GET)
}