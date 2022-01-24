<template>
  <div class="row">
    <Categories />
    <div class="col-12 col-md-9">
      <div v-if="posts.length > 0">
        <BlogItem v-for="post in posts" :key="post.id" :post="post" />
      </div>
      <div v-else class="alert alert-warning" role="alert">
        Bu kategoride post bulunmamaktadır.
      </div>
    </div>
  </div>
</template>

<script>
import BlogItem from "../components/BlogItem.vue";
import Categories from "../components/Categories.vue";
export default {
  data() {
    return {
      posts: [],
    };
  },
  mounted() {
    this.getPosts();
  },
  updated() {
    this.getPosts();
  },
  methods: {
    getPosts() {
      let url = "";
      let slug = this.$router.currentRoute._rawValue.query.category;
      if (slug) url += `?slug=${slug}`;

      this.$appAxios.get(`/posts${url}`).then((res) => (this.posts = res.data));
    },
  },
  components: { BlogItem, Categories },
};
</script>

<style>
</style>