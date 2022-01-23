import { createStore } from "vuex";

function parseJwt(token) {
    if (!token) return {
        exp: 0
    }
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

export default createStore({
    state: {
        token: null
    },
    getters: {
        _isAuth(state) {
            const data = parseJwt(state.token);
            let isAuth = new Date(data.exp * 1000) > new Date();
            if (!isAuth) {
                state.token = null;
            }
            return isAuth
        }
    }
})