<template>
  <form @submit.prevent="onSave" class="form-card mx-auto">
    <label for="category-name" class="mt-5 mb-3">Kategori Adı</label>
    <input
      v-model="userData.name"
      id="category-name"
      type="text"
      class="form-control mb-3"
    />
    <button class="btn btn-success mt-5 btn-block">Kaydet</button>
  </form>
</template>

<script>
export default {
  data() {
    return {
      userData: {
        name: null,
      },
    };
  },
  methods: {
    onSave() {
      console.log(this.userData);
    },
  },
};
</script>

<style>
</style>