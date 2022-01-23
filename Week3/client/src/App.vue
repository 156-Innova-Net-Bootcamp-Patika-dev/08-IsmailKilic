<template>
  <Navbar />
  <div class="main container">
    <router-view></router-view>
  </div>
</template>

<script>
import Navbar from "./components/Navbar.vue";
export default {
  components: { Navbar },
};
</script>

<style>
body{
  overflow-x: hidden;
}
.main{
  margin: 20px auto;
}
.form-card{
  width: min(100%, 500px);
}
</style>