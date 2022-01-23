<template>
  <div class="form-card mx-auto mt-5 card p-5">
    <h3 class="text-center mb-3">Giriş Yap</h3>
    <form @submit.prevent="onSave">
      <input
        v-model="userData.username"
        type="text"
        placeholder="Kullanıcı Adı"
        class="form-control mb-3"
      />
      <input
        v-model="userData.password"
        type="password"
        placeholder="Şifre"
        class="form-control mb-3"
      />
      <button class="btn btn-success btn-block">Giriş yap</button>
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
        username: null,
        password: null,
      },
    };
  },
};
</script>

<style>
</style>