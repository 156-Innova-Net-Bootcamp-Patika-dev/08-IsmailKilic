<template>
  <div class="card p-3 mb-3 d-flex flex-row flex-column flex-sm-row">
    <img
      width="200"
      style="max-height: 200px; object-fit: contain;"
      class="my-auto my-sm-0 mx-auto"
      :src="post.photo"
      alt="post-image"
    />
    <div class="ml-sm-3 col mt-3">
      <h5>{{post.title}}</h5>
      <p class="text-justify" v-html="post.body.substring(0, 200)+'...'">
      </p>
      <div class="d-flex justify-content-between mt-5">
        <div class="d-flex">
            <span class="mr-1 rounded border px-2">{{post.category.name}}</span>
            <span class="rounded border px-2">By {{post.user.fullName}}</span>
        </div>
        <router-link
          :to="{ name: 'SinglePost', params: { slug: post.slug } }"
          class="btn btn-sm btn-secondary"
          >Okumaya devam et...</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["post"],
  data() {
    return {
    };
  },
};
</script>