export const ENDPOINTS = {
    // AUTH
    USER_LOGIN: '/api/User/auth/login', //(POST)

    // USER
    GET_ALL_USER: '/api/User', //(GET)
    GET_ONE_USER: '/api/User/email', //(GET)
    CREATE_USER: '/api/User', //(POST)
    LOGOUT_USER: '/api/User/logout', //(DELETE)
    UPDATE_USER: '/api/User/email', //(PUT) + id
    UPDATE_USER_ID: '/api/User', // (PUT) + id
    REFRESH_TOKEN: '/api/User/refreshToken', //(POST)
    DELETE_USER: '/api/User', // (DELETE) + id

    // CATEGORY
    GET_ALL_CATEGORY: '/api/Category', //(GET)
    UPDATE_CATEGORY: '/api/Category', //(PUT) + add ID
    CREATE_CATEGORY: '/api/Category', // (POST)
    DELETE_CATEGORY: '/api/Category', //(DELETE) + add ID

    // TAG
    GET_ALL_TAG: '/api/Tag', //(GET)
    UPDATE_TAG: '/api/Tag', //(PUT) + add ID
    CREATE_TAG: '/api/Tag', //(POST) + add ID
    DELETE_TAG: '/api/Tag', //(DELETE) + add ID

    // ATTRIBUTE
    GET_ALL_ATTRIBUTE: '/api/Attribute', //(GET),
    UPDATE_ATTRIBUTE: '/api/Attribute', //(PUT) + add ID
    CREATE_ATTRIBUTE: '/api/Attribute', //(POST)
    DELETE_ATTRIBUTE: '/api/Attribute', //(DELETE) + add ID

    // ATTRIBUTE_DATA
    UPDATE_ATTRIBUTE_DATA: '/api/AttributeData', //(PUT) + add ID
    CREATE_ATTRIBUTE_DATA: '/api/AttributeData', //(POST)
    DELETE_ATTRIBUTE_DATA: '/api/AttributeData', //(DELETE) + add ID
}