<template>
  <div class="row">
    <Categories />
    <div class="col-12 col-md-9">
      <BlogItem v-for="i in 10" :key="i" :id="i" />
    </div>
  </div>
</template>

<script>
import BlogItem from "../components/BlogItem.vue";
import Categories from "../components/Categories.vue";
export default {
  mounted() {
    this.getPosts();
  },
  updated() {
    this.getPosts();
  },
  methods: {
    getPosts(){
      let url = "http://localhost:3000/api/posts"
      let category = this.$router.currentRoute._rawValue.query.category
      if(category) url += `?category=${category}`
      console.log(url);
    }
  },
  components: { BlogItem, Categories },
};
</script>

<style>
</style>