import { createRouter, createWebHashHistory } from 'vue-router';

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
        path: "/posts/:id",
        component: () => import("@/views/SinglePost")
    },
]

const router = createRouter({
    routes,
    history: createWebHashHistory()
})

export default router