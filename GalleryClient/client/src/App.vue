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
*,
*::after,
*::before {
  margin: 0;
  padding: 0;
  box-sizing: inherit;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
