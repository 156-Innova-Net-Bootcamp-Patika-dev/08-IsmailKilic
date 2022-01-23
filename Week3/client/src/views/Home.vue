<template>
  <div class="row">
    <div class="col-12 col-md-3 mb-5 mb-md-0">
      <ul class="list-group">
        <li class="list-group-item">
          <router-link :to="{ name: 'Home', query: { category: 'vuejs' } }"
            >An item</router-link
          >
        </li>
        <li class="list-group-item">
          <router-link :to="{ name: 'Home', query: { category: 'react' } }"
            >An item</router-link
          >
        </li>
        <li class="list-group-item">
          <router-link :to="{ name: 'Home', query: { category: 'nodejs' } }"
            >An item</router-link
          >
        </li>
      </ul>
    </div>
    <div class="col-12 col-md-9">
      <BlogItem v-for="i in 10" :key="i" :id="i" />
    </div>
  </div>
</template>

<script>
import BlogItem from "../components/BlogItem.vue";
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
  components: { BlogItem },
};
</script>

<style>
</style>