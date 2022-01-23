<template>
  <div class="form-card mx-auto mt-5 card p-5">
    <h3 class="text-center mb-3">Giriş Yap</h3>
    <form @submit.prevent="onSave">
      <input
        v-model="userData.email"
        type="email"
        placeholder="Email"
        class="form-control mb-3"
      />
      <input
        v-model="userData.password"
        type="password"
        placeholder="Şifre"
        class="form-control mb-3"
      />
      <div
        v-if="errors.length > 0"
        class="alert alert-danger mb-3"
        role="alert"
      >
        <div v-for="err in errors" :key="err">{{ err }}</div>
      </div>
      <button class="btn btn-success btn-block">
        <div v-if="loading" class="spinner-border" role="status"></div>
        <span v-else>Giriş Yap</span>
      </button>
      <span class="text-center mt-3 d-block">
        Üye değilim,
        <router-link :to="{ name: 'Register' }"
          >Üye olmak istiyorum!</router-link
        >
      </span>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      userData: {
        email: null,
        password: null,
      },
      loading: false,
      errors: [],
    };
  },
  methods: {
    async onSave() {
      this.loading = true;
      try {
        const res = await this.$appAxios.post("/auth/login", this.userData);
        this.$store.commit("setUser", res.data);
        this.$router.push({ name: "Home" });
      } catch (error) {
        const errs = error.response.data.errors || error.response.data.error;
        console.log(errs.length);
        this.errors = [];

        Object.keys(errs).map((key) => {
          if (typeof errs[key] === "string") this.errors.push(errs[key]);
          else {
            errs[key].forEach((err) => {
              this.errors.push(err);
            });
          }
        });
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>

<style>
</style>