<template>
  <div v-if="_isAuth" class="card col-md-8 p-4 mt-5">
    <h3>Yorum Ekle</h3>
    <form @submit.prevent="onSave">
      <input
        type="text"
        v-model="data.name"
        class="form-control mb-3 mt-2"
        placeholder="Adınız"
      />
      <textarea
        v-model="data.content"
        class="form-control mb-3"
        placeholder="Yorumunuz"
        cols="30"
        rows="5"
      ></textarea>
      <button class="btn btn-primary">Gönder</button>
    </form>
  </div>
  <div v-else class="alert alert-danger col-sm-8" role="alert">
    Yorum yapabilmek için oturum açmanız gerekmektedir.
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  props: ["postId"],
  data() {
    return {
      data: {
        name: null,
        content: null,
      },
    };
  },
  computed: {
    ...mapGetters(["_isAuth", "getUser"]),
  },
  methods: {
    async onSave() {
      await this.$appAxios.post("/comments", {
        ...this.data,
        userId: this.getUser.id,
        postId: this.postId,
      });
      this.$router.go(0);
    },
  },
};
</script>