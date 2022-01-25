<template>
  <div class="card mb-3 d-flex p-3 col-sm-8 flex-row">
    <div class="avatar"></div>
    <div class="ml-3">
      <div>
        <span class="name">{{ comment.name }}</span>
        <span class="date">{{ date }}</span>
      </div>
      <p class="content">
        {{ comment.content }}
      </p>
    </div>
  </div>
</template>

<script>
import moment from "moment";
export default {
  props: ["comment"],
  computed: {
    date() {
      return moment(new Date(this.comment.createdAt),"YYYYMMDD").fromNow();
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