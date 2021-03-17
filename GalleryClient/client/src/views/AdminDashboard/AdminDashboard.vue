<template>
  <div class="dashboard">
    <ul>
      <li>All Posts</li>
      <li>All Users</li>
      <li></li>
      <li></li>
      <li></li>
    </ul>
    Admin dashboard
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";

export default defineComponent({
  setup() {
    const state = reactive({
    });

    watchEffect(async () => {

    });

    return {
      state,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
