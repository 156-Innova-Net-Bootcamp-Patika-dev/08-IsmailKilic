<template>
  <div class="form-card mx-auto mt-5 card p-5">
    <h3 class="text-center mb-3">Kayıt Ol</h3>
    <form @submit.prevent="onSave">
      <input
        v-model="userData.fullName"
        type="text"
        placeholder="Tam Ad"
        class="form-control mb-3"
      />
      <input
        v-model="userData.email"
        autocomplete="email"
        type="email"
        placeholder="Email"
        class="form-control mb-3"
      />
      <input
        v-model="userData.password"
        autocomplete="current-password"
        type="password"
        placeholder="Şifre"
        class="form-control mb-3"
      />
      <Errors :errors="errors" />
      <button class="btn btn-success btn-block">
        <div v-if="loading" class="spinner-border" role="status"></div>
        <span v-else>Kayıt Ol</span>
      </button>
      <span class="text-center mt-3 d-block">
        Zaten Üyeyim,
        <router-link :to="{ name: 'Login' }">Giriş yap!</router-link>
      </span>
    </form>
  </div>
</template>

<script>
import Errors from "../components/Errors.vue";
export default {
  data() {
    return {
      userData: {
        fullName: null,
        email: null,
        password: null,
      },
      loading: false,
      errors: null,
    };
  },
  methods: {
    async onSave() {
      this.loading = true;
      try {
        await this.$appAxios.post("/auth/register", this.userData);
        this.$router.push({ name: "Login" });
      } catch (error) {
        this.errors = error;
      } finally {
        this.loading = false;
      }
    },
  },
  components: { Errors },
};
</script>
<style>
</style>