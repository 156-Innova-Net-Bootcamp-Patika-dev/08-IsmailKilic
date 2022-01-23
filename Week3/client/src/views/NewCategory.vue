<template>
  <form @submit.prevent="onSave" class="form-card mx-auto">
    <label for="category-name" class="mt-5 mb-3">Kategori Adı</label>
    <input
      v-model="userData.name"
      id="category-name"
      type="text"
      class="form-control mb-3"
    />
    <Errors :errors="errors"/>
    <button class="btn btn-success mt-3 btn-block">Kaydet</button>
  </form>
</template>

<script>
import Errors from "../components/Errors.vue";
export default {
  data() {
    return {
      userData: {
        name: null,
      },
      errors: null,
    };
  },
  methods: {
    async onSave() {
      try {
        await this.$appAxios.post("/categories", this.userData);
        this.$router.push({ name: "Home" });
      } catch (error) {
        this.errors = error;
      }
    },
  },
  components: { Errors },
};
</script>

<style>
</style>