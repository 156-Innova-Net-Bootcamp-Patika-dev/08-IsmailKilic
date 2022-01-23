<template>
  <div v-if="errorList.length > 0" class="alert alert-danger mb-3" role="alert">
    <div v-for="err in errorList" :key="err">{{ err }}</div>
  </div>
</template>

<script>
export default {
  props: ["errors"],
  data() {
    return {
      errorList: [],
    };
  },
  watch: {
    errors: function () {
      this.handleErrors();
    },
  },
  methods: {
    handleErrors() {
      if (!this.errors && this.errorList.length > 0) return;
      const errs =
        this.errors.response.data.errors || this.errors.response.data.error;
      this.errorList = [];
      Object.keys(errs).map((key) => {
        if (typeof errs[key] === "string") this.errorList.push(errs[key]);
        else {
          errs[key].forEach((err) => {
            this.errorList.push(err);
          });
        }
      });
    },
  },
};
</script>