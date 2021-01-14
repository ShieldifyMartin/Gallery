<template>
  <div class="submit-photo">
    <h1>Submit photo</h1>
    <form method="post" @submit.prevent="handleSubmit">
      <img
        v-if="state.pictureBase64"
        :src="state.pictureBase64"
        class="uploaded-image"
        alt="upload"
      />
      <label for="file" class="upload-label">choose a picture</label>
      <input
        type="file"
        id="file"
        class="file"
        size="80"
        ref="picture"
        @change="handleFileUpload"
      />
      <input
        type="text"
        v-model="state.location"
        name="location"
        id="location"
        placeholder="Location"
      />
      <input
        type="text"
        v-model="state.description"
        name="description"
        placeholder="Description"
        required
      />
      <select v-model="state.categoryId" class="categories-dropdown">
        <option value="" selected>Select category</option>
        <option
          v-for="category in state.categories"
          :key="category.id"
          :value="category.id"
          >{{ category.title }}</option
        >
      </select>
      <input type="submit" value="Create" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import Swal from "sweetalert2";
import router from "../../router";
import store from "../../store";
import { postService, categoryService } from "../../services";
import validate from "./validator";

export default defineComponent({
  setup() {
    const state = reactive({
      categories: [],
      picture: null,
      pictureBase64: null,
      location: "",
      description: "",
      categoryId: "",
      maxSize: 15728640,
      isMobile: screen.width <= 700
    });

    watchEffect(async () => {
      const categories = await categoryService.get();

      state.categories = categories;
    });

    function handleFileUpload(e) {
      let file = e.target.files[0];

      if (!e.target.files.length) return;

      if (e.target.files.length === 1) {
        if (file.size > state.maxSize) {
          Swal.fire({
            position: state.isMobile ? "top" : "top-end",
            icon: "error",
            title: "Too large picture!",
            showConfirmButton: false,
            timer: 1500,
            width: state.isMobile ? 250 : 300,
          });
        } else {
          state.picture = file;
          var fr = new FileReader();
          fr.readAsDataURL(file);
          fr.onload = () => {
            state.pictureBase64 = fr.result;
          };
        }
      } else {
        Swal.fire({
          position: state.isMobile ? "top" : "top-end",
          icon: "error",
          title: "Only one photo is allowed!",
          showConfirmButton: false,
          timer: 1500,
          width: state.isMobile ? 250 : 300,
        });
      }
    }

    async function handleSubmit() {
      const { picture, location, description, categoryId } = state;
      const isCorrect = validate(state);

      if (!isCorrect) {
        return;
      }

      var response = await postService.create(
        store.state.auth.state.token,
        picture,
        location,
        description,
        categoryId
      );

      if (response == 401) {
        router.push("/login");
      } else if (response >= 400) {       
       Swal.fire({
          position: state.isMobile ? "top" : "top-end",
          icon: "error",
          title: "Something went wrong!",
          showConfirmButton: false,
          timer: 1500,
          width: state.isMobile ? 250 : 300,
        });
      } else {        
        Swal.fire({
          position: state.isMobile ? "top" : "top-end",
          icon: "success",
          title: "Successful!",
          showConfirmButton: false,
          timer: 1500,
          width: state.isMobile ? 250 : 300,
        });
        router.push("/");
      }
    }

    return {
      state,
      handleSubmit,
      handleFileUpload
    };
  },
});
</script>

<styles src="./index.scss"></styles>
