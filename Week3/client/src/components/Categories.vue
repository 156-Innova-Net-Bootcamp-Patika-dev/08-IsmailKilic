<template>
  <div class="col-12 col-md-3 mb-5 mb-md-0">
    <ul class="list-group">
      <li v-for="cat in categories" :key="cat.id" class="list-group-item">
        <router-link :to="{ name: 'Home', query: { category: cat.slug } }">{{
          cat.name
        }}</router-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      categories: [],
    };
  },
  async created() {
    const res = await this.$appAxios.get("/categories");
    this.categories = res.data;
  },
};
</script>