<template>
  <div id="app">
    <app-header />
    <router-view />    
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import AppHeader from "./components/core/Header/Header.vue";

export default defineComponent({
  name: "Home",
  components: {
    AppHeader,    
  },
});
</script>

<style lang="scss">
@import url('https://fonts.googleapis.com/css2?family=Work+Sans:wght@400;500&display=swap');
*,
*::after,
*::before {
  margin: 0;
  padding: 0;
  box-sizing: inherit;
}

#app {
  font-family: 'Work Sans', sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
