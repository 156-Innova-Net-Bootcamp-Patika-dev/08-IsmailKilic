<template>
  <form @submit.prevent="onSave" class="post-card mx-auto">
    <label for="post-title" class="mb-3">Başlık</label>
    <input
      v-model="data.title"
      id="post-title"
      type="text"
      class="form-control mb-3"
    />
    <label for="post-photo" class="mb-3">Kapak Fotoğrafı</label>
    <input
      type="url"
      id="post-photo"
      v-model="data.photo"
      class="form-control mb-3"
    />
    <label for="post-category" class="mb-3">Kategori</label>
    <select
      v-model="data.category"
      class="form-control mb-3"
      id="post-category"
    >
      <option disabled value="" selected>Category</option>
      <option v-for="cat in categories" :key="cat.id" :value="cat.id">
        {{ cat.name }}
      </option>
    </select>
    <label for="post-body" class="mb-3">Post İçeriği</label>
    <ckeditor
      id="post-body"
      :editor="editor"
      v-model="data.body"
      :config="editorConfig"
    ></ckeditor>
    <button class="btn btn-success mt-5 btn-block">Kaydet</button>
  </form>
</template>

<script>
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";

export default {
  data() {
    return {
      categories: [],
      editor: ClassicEditor,
      data: {
        title: null,
        category: null,
        body: null,
        photo: null,
      },
      editorConfig: {
        sidebar: {
          preventScrollOutOfView: true,
        },
      },
    };
  },
  async created() {
    const res = await this.$appAxios.get("/categories");
    this.categories = res.data;
  },
  methods: {
    onSave() {
      console.log(this.data);
    },
  },
};
</script>

<style>
.post-card {
  width: min(100%, 750px);
}
</style>

