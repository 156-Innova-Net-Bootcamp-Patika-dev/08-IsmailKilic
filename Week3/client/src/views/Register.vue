<template>
  <div class="form-card mx-auto mt-5 card p-5">
    <h3 class="text-center mb-3">Kayıt Ol</h3>
    <form @submit.prevent="onSave">
      <input
        v-model="userData.fullname"
        type="text"
        placeholder="Tam Ad"
        class="form-control mb-3"
      />
      <input
        v-model="userData.username"
        autocomplete="username"
        type="text"
        placeholder="Kullanıcı Adı"
        class="form-control mb-3"
      />
      <input
        v-model="userData.password"
        autocomplete="current-password"
        type="password"
        placeholder="Şifre"
        class="form-control mb-3"
      />
      <button  class="btn btn-success btn-block">Kayıt ol</button>
      <span class="text-center mt-3 d-block">
        Zaten Üyeyim,
        <router-link :to="{ name: 'Login' }">Giriş yap!</router-link>
      </span>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      userData: {
        fullname: null,
        username: null,
        password: null,
      },
    };
  },
};
</script>
<style>
</style>