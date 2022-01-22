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
]

const router = createRouter({
    routes,
    history: createWebHashHistory()
})

export default router