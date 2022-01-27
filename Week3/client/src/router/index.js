import { createRouter, createWebHistory } from 'vue-router';
import store from '../store';

const routes = [
    {
        name: "Home",
        path: "/",
        component: () => import("@/views/Home")
    },
    {
        name: "Login",
        path: "/login",
        component: () => import("@/views/Login")
    },
    {
        name: "Register",
        path: "/register",
        component: () => import("@/views/Register")
    },
    {
        name: "NewPost",
        path: "/new-post",
        component: () => import("@/views/NewPost")
    },
    {
        name: "NewCategory",
        path: "/new-category",
        component: () => import("@/views/NewCategory")
    },
    {
        name: "SinglePost",
        path: "/posts/:slug",
        component: () => import("@/views/SinglePost")
    },
]

const router = createRouter({
    routes,
    history: createWebHistory()
})

router.beforeEach((to, _, next) => {
    const authRequired = ["NewPost", "NewCategory"]
    const authNotRequired = ["Login", "Register"]
    const _isAuth = store.getters._isAuth;

    if (authNotRequired.indexOf(to.name) > -1 && _isAuth) next(false);

    if (authRequired.indexOf(to.name) > -1) {
        if (_isAuth) next();
        else next({ name: "Login" });
    }
    else {
        next();
    }
})

export default router