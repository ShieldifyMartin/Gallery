<template>
  <div id="nav">
    <router-link to="/">Home</router-link>|
    <router-link to="/login">Login</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "Header",
});
</script>
