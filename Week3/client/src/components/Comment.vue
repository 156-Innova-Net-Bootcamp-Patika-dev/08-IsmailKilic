<template>
  <div class="card mb-3 d-flex p-3 col-md-8 flex-row">
    <div class="avatar"></div>
    <div class="ml-3 col">
      <div>
        <span class="name">{{ comment.name }}</span>
        <span class="date">{{ date }}</span>
      </div>
      <p class="content">
        {{ comment.content }}
      </p>
    </div>
    <div v-if="isMyComment">
      <button @click="onDelete" class="btn btn-sm btn-danger">X</button>
    </div>
  </div>
</template>

<script>
import moment from "moment";
import { mapGetters } from "vuex";
export default {
  props: ["comment"],
  computed: {
    ...mapGetters(["getUser"]),
    date() {
      return moment(new Date(this.comment.createdAt), "YYYYMMDD").fromNow();
    },
    isMyComment() {
      return this.comment?.user.id === this.getUser?.id;
    },
  },
  methods: {
    async onDelete() {
      await this.$appAxios.delete(`/comments/${this.comment.id}`);
      this.$router.go(0);
    },
  },
};
</script>

<style>
.avatar {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  background-color: black;
}
.name {
  font-weight: bold;
  margin-right: 8px;
}
.date {
  font-size: small;
  color: #555;
}
.content {
  color: #555;
}
</style>