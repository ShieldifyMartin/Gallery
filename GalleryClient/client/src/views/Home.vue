<template>
  <div class="home">
    <h1>Home page</h1>
    isAuth: {{isAuth}}
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Home',
  computed: {
    isAuth () {      
      return this.$store.state.auth.state.token !== null
    }
  }
});
</script>
