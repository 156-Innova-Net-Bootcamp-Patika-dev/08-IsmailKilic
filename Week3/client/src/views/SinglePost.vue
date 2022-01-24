<template>
  <h1>{{ post.title }}</h1>
  <div class="mt-3">
    <span class="mr-1 rounded border px-2">{{ post.category.name }}</span>
    <span class="rounded mr-1 border px-2">{{ post.user.fullName }}</span>
    <span class="rounded border px-2">{{ date }}</span>
  </div>
  <img :src="post.photo" class="img-fluid my-4 mx-auto" alt="" />
  <div class="text-justify" v-html="post.body"></div>

  <AddComment />
</template>

<script>
import moment from "moment";
import AddComment from "../components/AddComment.vue";
moment.locale("tr");
export default {
  data() {
    return {
      post: null,
    };
  },
  async created() {
    const slug = this.$router.currentRoute._rawValue.params.slug;
    const res = await this.$appAxios.get(`/posts/${slug}`);
    this.post = res.data;
  },
  computed: {
    date() {
      return moment(this.post.createdAt).format("Do MMM, YYYY");
    },
  },
  components: { AddComment },
};
</script>