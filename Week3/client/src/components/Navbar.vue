<template>
  <nav class="navbar sticky-top navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
      <router-link
        :to="{ name: 'Home' }"
        active-class="active"
        class="navbar-brand"
        >Blog</router-link
      >
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarScroll"
        aria-controls="navbarScroll"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarScroll">
        <ul class="navbar-nav mr-auto my-2 my-lg-0 navbar-nav-scroll"></ul>
        <ul v-if="_isAuth" class="navbar-nav my-2 my-lg-0 navbar-nav-scroll">
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              id="navbarScrollingDropdown"
              role="button"
              data-toggle="dropdown"
              aria-expanded="false"
            >
              Profil
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarScrollingDropdown">
              <li>
                <router-link :to="{ name: 'NewCategory' }" class="dropdown-item"
                  >Kategori Ekle</router-link
                >
              </li>
              <li>
                <router-link :to="{ name: 'NewPost' }" class="dropdown-item"
                  >Post Ekle</router-link
                >
              </li>
            </ul>
          </li>
          <li class="nav-item">
            <router-link
              :to="{ name: 'Login' }"
              class="nav-link"
              active-class="active"
              >Çıkış Yap</router-link
            >
          </li>
        </ul>
        <ul v-else class="navbar-nav my-2 my-lg-0 navbar-nav-scroll">
          <li class="nav-item">
            <router-link
              :to="{ name: 'Login' }"
              class="nav-link"
              active-class="active"
              >Giriş Yap</router-link
            >
          </li>
          <li class="nav-item">
            <router-link
              :to="{ name: 'Register' }"
              class="nav-link"
              active-class="active"
              >Kayıt Ol</router-link
            >
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters(["_isAuth"]),
  },
};
</script>