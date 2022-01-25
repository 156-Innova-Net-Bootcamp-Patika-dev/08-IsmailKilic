<template>
  <h6>Yorumlar ( {{ comments?.length || 0 }} )</h6>
  <comment v-for="cmnt in comments" :key="cmnt.id" :comment="cmnt" />
</template>

<script>
import Comment from "./Comment.vue";
export default {
  props: ["comments"],
  components: { Comment },
};
</script>

<style>
</style>