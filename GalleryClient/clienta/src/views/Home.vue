<template>
  <div class="home">
    <h1>Home page</h1>
    <v-banner>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent cursus nec sem id malesuada.
      Curabitur lacinia sem et turpis euismod, eget elementum ex pretium.
      <template v-slot:actions>
        <v-btn text color="primary">Dismiss</v-btn>
        <v-btn text color="primary">Retry</v-btn>
      </template>
    </v-banner>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Home'
});
</script>
