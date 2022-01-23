<template>
  <div class="card p-3 mb-3 d-flex flex-row flex-column flex-sm-row">
    <img
      width="200"
      style="max-height: 200px"
      class="my-auto my-sm-0 mx-auto img-fluid"
      src="https://www.instandngs4p.eu/wp-content/themes/fox/images/placeholder.jpg"
      alt="post-image"
    />
    <div class="ml-sm-3 col mt-3">
      <h5>Title</h5>
      <p class="text-justify">
        {{ data.substring(0, 200) + "..." }}
      </p>
      <div class="d-flex justify-content-end mt-5">
        <router-link
          :to="{ name: 'SinglePost', params: { id } }"
          class="btn btn-sm btn-secondary"
          >Okumaya devam et...</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["id"],
  data() {
    return {
      data: `Lorem ipsum dolor sit amet consectetur adipisicing elit. Corrupti labore
        impedit dignissimos obcaecati itaque at explicabo, quibusdam incidunt
        ipsum qui minus placeat sint quae magnam et error voluptate quis non`,
    };
  },
};
</script>